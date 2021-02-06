<template>
  <div>
    <el-button type="primary" size="small" icon="el-icon-plus" @click="dialogAddFormVisible = true">增加</el-button>
    <el-table
      v-loading="loading"
      :size="tableSize"
      :data="tableData"
      class="w-100"
      stripe
    >
      <el-table-column
        prop="name"
        label="权限"
      />
      <el-table-column
        prop="display_name"
        label="权限名"
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
    <el-dialog title="新增" :visible.sync="dialogAddFormVisible" width="40%">
      <el-form ref="addForm" :model="addForm" :rules="addRules" :size="formSize">
        <el-form-item :label-width="formLabelWidth">
          <el-radio-group v-model="defaultPermission" @change="setPermission">
            <el-radio-button v-for="item in defaultPermissions" :key="item.value" :label="item.value">{{ item.label }}</el-radio-button>
          </el-radio-group>
        </el-form-item>
        <el-form-item label="权限" prop="name" :label-width="formLabelWidth">
          <el-input v-model="addForm.name">
            <template slot="prepend">{{ permissionGroupName }}.</template>
          </el-input>
        </el-form-item>
        <el-form-item label="权限名" prop="display_name" :label-width="formLabelWidth">
          <el-input v-model="addForm.display_name" />
        </el-form-item>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button :size="buttonSize" @click="dialogAddFormVisible = false">取消</el-button>
        <el-button type="primary" :size="buttonSize" :loading="buttonLoading" @click="handleAdd">确认</el-button>
      </div>
    </el-dialog>
    <el-dialog title="修改" :visible.sync="dialogEditFormVisible" width="40%">
      <el-form ref="editForm" :model="editForm" :rules="editRules" :size="formSize">
        <el-form-item label="权限" prop="name" :label-width="formLabelWidth">
          <el-input v-model="editForm.name" />
        </el-form-item>
        <el-form-item label="权限名" prop="display_name" :label-width="formLabelWidth">
          <el-input v-model="editForm.display_name" />
        </el-form-item>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button :size="buttonSize" @click="dialogEditFormVisible = false">取消</el-button>
        <el-button type="primary" :size="buttonSize" :loading="buttonLoading" @click="handleUpdate">确认</el-button>
      </div>
    </el-dialog>
  </div>
</template>

<script>
import { queryParams } from '@/mixins/queryParams'
import { responseDataFormat, simpleAdd, simpleDelete, simpleUpdate } from '@/utils/viewIndexHanlder'
import { list, add, update, destroy } from '@/api/permission'
import { alphaDash } from '@/utils/rules'

export default {
  name: 'Permission',
  mixins: [queryParams],
  props: {
    permissionGroupId: {
      type: Number,
      required: true
    },
    permissionGroupName: {
      type: String,
      required: true
    }
  },
  data() {
    return {
      defaultPermission: null,
      defaultPermissions: [
        { label: '首页', value: 'index' },
        { label: '新增', value: 'add' },
        { label: '修改', value: 'update' },
        { label: '删除', value: 'delete' }
      ],
      addForm: {
        name: '',
        display_name: ''
      },
      addRules: {
        name: [
          { required: true, ...alphaDash('请正确输入权限，不能是中文') }
        ],
        display_name: [
          { required: true }
        ]
      },
      editForm: {
        name: '',
        display_name: ''
      },
      editRules: {
        name: [
          { required: true, ...alphaDash('请正确输入权限，不能是中文') }
        ],
        display_name: [
          { required: true }
        ]
      }
    }
  },
  watch: {
    permissionGroupId() {
      this.defaultPermission = 'index'
      this.addForm = { name: '', display_name: '' }
      this.getList()
    }
  },
  methods: {
    getList() {
      this.loading = true
      list({ group_id: this.permissionGroupId, page: this.queryPage, page_size: this.pagination.pageSize }).then(resp => {
        responseDataFormat(resp, this)
      })
    },
    handleAdd() {
      this.addForm['group_id'] = this.permissionGroupId
      this.addForm['name'] = `${this.permissionGroupName}.${this.addForm['name']}`
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
    setPermission(item) {
      const checked = this.defaultPermissions.find(t => t.value === item)
      this.addForm = { name: checked.value, display_name: checked.label }
    }
  }
}
</script>
