<template>
  <el-card class="app-container">
    <el-row>
      <el-col :span="22">
        <el-form :inline="true" :size="searchFormSize" :model="queryParams">
          <el-form-item label="名称">
            <el-input v-model="queryParams.name" clearable />
          </el-form-item>
          <el-form-item>
            <el-button type="primary" icon="el-icon-search" @click="getList">查找</el-button>
          </el-form-item>
        </el-form>
      </el-col>
      <el-col :span="2" class="dp-flex fd-rr">
        <el-button :size="buttonSize" type="primary" icon="el-icon-plus" @click="dialogAddFormVisible = true">新增</el-button>
      </el-col>
    </el-row>
    <el-table
      v-loading="loading"
      :size="tableSize"
      :data="tableData"
      class="w-100"
      stripe
    >
      <el-table-column
        prop="username"
        label="用户名"
      />
      <el-table-column
        prop="name"
        label="名称"
      />
      <el-table-column
        prop="roles"
        label="角色"
      />
      <el-table-column
        prop="created_time"
        label="创建时间"
      />
      <el-table-column
        prop="updated_time"
        label="修改时间"
      />
      <el-table-column
        fixed="right"
        width="300px"
        label="操作"
      >
        <template slot-scope="scope">
          <el-button
            :size="buttonSize"
            @click="handleEdit(scope.$index, scope.row)"
          >
            编辑
          </el-button>
          <el-button
            :size="buttonSize"
            @click="handleAssignRole(scope.row)"
          >
            分配角色
          </el-button>
          <el-button
            :size="buttonSize"
            type="danger"
            @click="handleDelete(scope.row.id)"
          >
            删除
          </el-button>
        </template>
      </el-table-column>
    </el-table>
    <el-pagination
      :current-page.sync="pagination.currentPage"
      :page-size="pagination.pageSize"
      layout="total, prev, pager, next, jumper"
      :total="pagination.total"
      @current-change="getList"
    />
    <el-dialog title="新增" :visible.sync="dialogAddFormVisible" width="50%">
      <el-form ref="addForm" :model="addForm" :rules="addRules" :size="formSize">
        <el-form-item label="用户名" prop="username" :label-width="formLabelWidth">
          <el-input v-model="addForm.username" />
        </el-form-item>
        <el-form-item label="密码" prop="password" :label-width="formLabelWidth">
          <el-input v-model="addForm.password" type="password" />
        </el-form-item>
        <el-form-item label="名称" prop="name" :label-width="formLabelWidth">
          <el-input v-model="addForm.name" />
        </el-form-item>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button :size="buttonSize" @click="dialogAddFormVisible = false">取消</el-button>
        <el-button type="primary" :size="buttonSize" :loading="buttonLoading" @click="handleAdd">确认</el-button>
      </div>
    </el-dialog>
    <el-dialog title="修改" :visible.sync="dialogEditFormVisible" width="50%">
      <el-form ref="editForm" :model="editForm" :rules="editRules" :size="formSize">
        <el-form-item label="密码" prop="password" :label-width="formLabelWidth">
          <el-input v-model="editForm.password" type="password" />
        </el-form-item>
        <el-form-item label="名称" prop="name" :label-width="formLabelWidth">
          <el-input v-model="editForm.name" />
        </el-form-item>
        <el-form-item label="头像" prop="avatar" :label-width="formLabelWidth">
          <upload-image v-model="editForm.avatar" :value="editForm.avatar" />
        </el-form-item>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button :size="buttonSize" @click="dialogEditFormVisible = false">取消</el-button>
        <el-button type="primary" :size="buttonSize" :loading="buttonLoading" @click="handleUpdate">确认</el-button>
      </div>
    </el-dialog>
    <el-dialog title="分配角色" :visible.sync="dialogAssignRoleFormVisible" width="40%">
      <el-form ref="assignRoleForm" :model="assignRoleForm" :rules="assignRoleRules" :size="formSize">
        <el-select v-model="assignRoleForm.role_ids" multiple placeholder="请选择" class="w-100">
          <el-option
            v-for="item in roleOptions"
            :key="item.value"
            :label="item.label"
            :value="item.value"
          />
        </el-select>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button :size="buttonSize" @click="dialogAssignRoleFormVisible = false">取消</el-button>
        <el-button type="primary" :size="buttonSize" :loading="buttonLoading" @click="assignRole">确认</el-button>
      </div>
    </el-dialog>
  </el-card>
</template>
<script>
import { queryParams } from '@/mixins/queryParams'
import {
  responseDataFormat,
  simpleUpdate,
  simpleAdd,
  simpleDelete
} from '@/utils/viewIndexHanlder'
import { list, add, update, destroy, assignRole } from '@/api/administrator'
import { roles } from '@/api/selector'
import UploadImage from '@/components/Upload/UploadImage'

export default {
  components: { UploadImage },
  mixins: [queryParams],
  data() {
    const requiredAndBetween = [
      { required: true },
      { min: 3, max: 20 }
    ]
    return {
      dialogAssignRoleFormVisible: false,
      addForm: {
        username: '',
        name: '',
        password: '',
        avatar: ''
      },
      addRules: {
        name: requiredAndBetween,
        username: requiredAndBetween,
        password: [
          { required: true },
          { min: 6, max: 32 }
        ]
      },
      editForm: {
        name: '',
        password: ''
      },
      editRules: {
        name: requiredAndBetween,
        username: requiredAndBetween
      },
      assignRoleForm: {
        role_ids: []
      },
      assignRoleRules: {
        role_ids: []
      },
      roleOptions: []
    }
  },
  mounted() {
    this.getList()
    this.initSelector()
  },
  methods: {
    getList() {
      this.loading = true
      list({ ...this.queryParams, page: this.queryPage, page_size: this.pagination.pageSize }).then(resp => {
        responseDataFormat(resp, this)
      })
    },
    handleAdd() {
      simpleAdd(this, add)
    },
    handleEdit(index, row) {
      this.editForm = row
      this.nowRowData = { index, row }
      this.dialogEditFormVisible = true
    },
    handleUpdate() {
      simpleUpdate(this, update)
    },
    handleDelete(id) {
      simpleDelete(this, destroy(id))
    },
    handleAssignRole(row) {
      this.assignRoleForm = row
      this.nowRowData = { row }
      this.dialogAssignRoleFormVisible = true
    },
    initSelector() {
      roles().then(res => {
        this.roleOptions = res.data
      })
    },
    assignRole() {
      this.buttonLoading = true
      assignRole(this.nowRowData.row.id, { role_ids: this.assignRoleForm.role_ids })
        .then(() => {
          this.$message.success('分配角色成功')
          this.buttonLoading = false
          this.dialogAssignRoleFormVisible = false
          this.getList()
        }).catch(() => {
          this.buttonLoading = false
        })
    }
  }
}
</script>
