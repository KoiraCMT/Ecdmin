<template>
  <div>
    <el-button type="primary" size="small" icon="el-icon-plus" @click="dialogAddFormVisible = true">增加</el-button>
    <el-row v-loading="loading" class="mt-10">
      <el-row v-for="item in tableData" :key="item.id" type="flex" class="ai-c">
        <el-col :span="16">
          <el-link @click="handleDisplayNameClick(item)">{{ item.display_name }}</el-link>
        </el-col>
        <el-col :span="8">
          <el-button type="text" :size="buttonSize" @click="handleEdit(item)">修改</el-button>
          <el-button type="text" :size="buttonSize" class="button--danger" @click="handleDelete(item.id)">删除</el-button>
        </el-col>
      </el-row>
      <el-pagination
        :current-page.sync="pagination.currentPage"
        :page-size="pagination.pageSize"
        layout="total, prev, pager, next"
        :total="pagination.total"
        @current-change="getList"
      />
    </el-row>
    <el-dialog title="新增" :visible.sync="dialogAddFormVisible" width="40%">
      <el-form ref="addForm" :model="addForm" :rules="addRules" :size="formSize">
        <el-form-item label="权限组" prop="name" :label-width="formLabelWidth">
          <el-input v-model="addForm.name" />
        </el-form-item>
        <el-form-item label="权限组名" prop="display_name" :label-width="formLabelWidth">
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
        <el-form-item label="名称" prop="name" :label-width="formLabelWidth">
          <el-input v-model="editForm.name" />
        </el-form-item>
        <el-form-item label="权限组名" prop="display_name" :label-width="formLabelWidth">
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
import { list, add, update, destroy } from '@/api/permissionGroup'
import { requiredAndBetween } from '@/utils/rules'
import { head } from 'lodash'

export default {
  name: 'PermissionGroup',
  mixins: [queryParams],
  data() {
    return {
      addForm: {
        name: '',
        display_name: ''
      },
      addRules: {
        name: requiredAndBetween(2, 20),
        display_name: requiredAndBetween(2, 20)
      },
      editForm: {
        name: '',
        display_name: ''
      },
      editRules: {
        name: requiredAndBetween(2, 20),
        display_name: requiredAndBetween(2, 20)
      }
    }
  },
  watch: {
    tableData(val) {
      if (val.length > 0) {
        this.handleDisplayNameClick(head(val))
      }
    }
  },
  created() {
    this.getList()
  },
  methods: {
    handleAdd() {
      simpleAdd(this, add)
    },
    handleEdit(group) {
      this.editForm = { name: group.name, display_name: group.display_name }
      this.nowRowData.row = group
      this.dialogEditFormVisible = true
    },
    handleUpdate() {
      simpleUpdate(this, update)
    },
    getList() {
      this.loading = true
      list({ page: this.queryPage, page_size: this.pagination.pageSize }).then(resp => {
        responseDataFormat(resp, this)
      })
    },
    handleDisplayNameClick(item) {
      this.$emit('get-id', item.id)
      this.$emit('get-name', item.name)
    },
    handleDelete(id) {
      simpleDelete(this, destroy, id)
    }
  }
}
</script>

<style scoped>

</style>
