<template>
  <el-card class="app-container">
    <el-row>
      <el-col :span="22">
        <el-form :inline="true" :size="searchFormSize" :model="queryParams">
          <el-form-item label="角色名">
            <el-input v-model="queryParams.display_name" clearable />
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
        prop="display_name"
        label="角色名"
      />
      <el-table-column
        prop="name"
        label="角色"
      />
      <el-table-column
        prop="description"
        label="描述"
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
            @click="handleAssignPermission(scope.row)"
          >
            分配权限
          </el-button>
          <el-button
            v-if="scope.row.name !== 'admin'"
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
        <el-form-item label="角色" prop="name" :label-width="formLabelWidth">
          <el-input v-model="addForm.name" />
        </el-form-item>
        <el-form-item label="角色名" prop="display_name" :label-width="formLabelWidth">
          <el-input v-model="addForm.display_name" />
        </el-form-item>
        <el-form-item label="描述" prop="description" :label-width="formLabelWidth">
          <el-input v-model="addForm.description" type="textarea" />
        </el-form-item>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button :size="buttonSize" @click="dialogAddFormVisible = false">取消</el-button>
        <el-button type="primary" :size="buttonSize" :loading="buttonLoading" @click="handleAdd">确认</el-button>
      </div>
    </el-dialog>
    <el-dialog title="修改" :visible.sync="dialogEditFormVisible" width="50%">
      <el-form ref="editForm" :model="editForm" :rules="editRules" :size="formSize">
        <el-form-item label="角色" prop="name" :label-width="formLabelWidth">
          <el-input v-model="editForm.name" />
        </el-form-item>
        <el-form-item label="角色名" prop="display_name" :label-width="formLabelWidth">
          <el-input v-model="editForm.display_name" />
        </el-form-item>
        <el-form-item label="描述" prop="description" :label-width="formLabelWidth">
          <el-input v-model="editForm.description" type="textarea" />
        </el-form-item>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button :size="buttonSize" @click="dialogEditFormVisible = false">取消</el-button>
        <el-button type="primary" :size="buttonSize" :loading="buttonLoading" @click="handleUpdate">确认</el-button>
      </div>
    </el-dialog>
    <el-dialog title="分配权限" :visible.sync="dialogAssignPermissionVisible" width="80%">
      <assign-permission :id="nowRowData.row.id" v-model="dialogAssignPermissionVisible" />
    </el-dialog>
  </el-card>
</template>

<script>
import { queryParams } from '@/mixins/queryParams'
import { add, destroy, list, update } from '@/api/role'
import { simpleAdd, simpleDelete, simpleList, simpleUpdate } from '@/utils/viewIndexHanlder'
import { alphaDash } from '@/utils/rules'
import AssignPermission from '@/views/role/assignPermission'

export default {
  name: 'Index',
  components: { AssignPermission },
  mixins: [queryParams],
  data() {
    return {
      dialogAssignPermissionVisible: false,
      addForm: {
        name: '',
        display_name: '',
        description: ''
      },
      addRules: {
        name: [
          { required: true, ...alphaDash('请正确输入角色,不能是中文') }
        ],
        display_name: [
          { required: true }
        ]
      },
      editForm: {
        name: '',
        display_name: '',
        description: ''
      },
      editRules: {
        name: [
          { required: true, ...alphaDash('请正确输入角色,不能是中文') }
        ],
        display_name: [
          { required: true }
        ]
      }
    }
  },
  mounted() {
    this.getList()
  },
  methods: {
    getList() {
      simpleList(this, list)
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
      simpleDelete(this, destroy, id)
    },
    handleAssignPermission(row) {
      this.dialogAssignPermissionVisible = true
      this.nowRowData.row = row
    }
  }
}
</script>

<style scoped>

</style>
