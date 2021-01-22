<template>
  <div v-loading="loading" class="container">
    <el-card v-for="group in groupPermissions" :key="group.id">
      <div slot="header">
        <el-checkbox
          v-model="radio[group.id].checked"
          :indeterminate="radio[group.id].isIndeterminate"
          @change="handleCheckAllChange(group.id)"
        >全选</el-checkbox>
        <span class="ml-15">{{ group.name }}</span>
      </div>
      <el-row>
        <el-checkbox-group v-model="rolePermissions" @change="handleCheckedRolePermissionsChange(group.id)">
          <el-col
            v-for="permission in group.permissions"
            :key="permission.id"
            :span="4"
          >
            <el-checkbox :label="permission.id">
              {{ permission.display_name }}
            </el-checkbox>
          </el-col>
        </el-checkbox-group>
      </el-row>
    </el-card>
    <div class="dp-flex fd-rr">
      <el-button :size="buttonSize" type="primary" @click="assign">分配</el-button>
      <el-button class="mr-15" :size="buttonSize" @click="cancel">取消</el-button>
    </div>
  </div>
</template>

<script>
import { listWithPermissions } from '@/api/permissionGroup'
import { has } from 'lodash'
import { assignPermission, findWithPermissions } from '@/api/role'

export default {
  name: 'AssignPermission',
  props: {
    id: {
      type: Number,
      required: true
    }
  },
  data() {
    return {
      buttonSize: 'small',
      loading: false,
      groupPermissions: {},
      rolePermissions: [],
      radio: {},
      role: {},
      groupIdWithPermissionIds: {},
      buttonLoading: false
    }
  },
  watch: {
    id() {
      this.findRoleWithPermissions()
    }
  },
  created() {
    this.getPermissions()
  },
  methods: {
    findRoleWithPermissions() {
      this.rolePermissions = []
      this.loading = true
      this.resetRadio()
      findWithPermissions(this.id).then(res => {
        const group_id = []
        res.data.permissions.forEach(item => {
          this.rolePermissions.push(item.id)
          if (group_id.indexOf(item.permission_group_id) === -1) {
            group_id.push(item.permission_group_id)
          }
        })
        group_id.forEach(id => this.handleCheckedRolePermissionsChange(id))
        this.loading = false
      }).catch(() => {
        this.loading = false
      })
    },
    resetRadio() {
      for (const groupId in this.groupIdWithPermissionIds) {
        this.radio[groupId].checked = false
        this.radio[groupId].isIndeterminate = false
      }
    },
    getPermissions() {
      this.loading = true
      listWithPermissions().then(resp => {
        resp.data.forEach(group => {
          this.groupPermissions[group.id] = group
          this.groupIdWithPermissionIds[group.id] = []
          group.permissions.forEach(permission => {
            this.groupIdWithPermissionIds[group.id].push(permission.id)
          })
          if (!has(this.radio, group.id)) {
            this.$set(this.radio, group.id, {
              checked: false,
              isIndeterminate: false
            })
          }
        })
        this.findRoleWithPermissions()
      }).catch(() => {
        this.loading = false
      })
    },
    handleCheckAllChange(groupId) {
      const permissions = this.groupIdWithPermissionIds[groupId]
      const checked = permissions.filter(item => this.rolePermissions.includes(item))
      if (checked.length === 0) {
        this.rolePermissions.push(...permissions)
      } else {
        const checkedLen = checked.length
        const permissionsLen = permissions.length
        permissions.forEach(p => {
          const index = this.rolePermissions.indexOf(p)
          if (checkedLen === permissionsLen) {
            this.rolePermissions.splice(index, 1)
          } else if (index === -1) {
            this.rolePermissions.push(p)
            this.radio[groupId].checked = true
            this.radio[groupId].isIndeterminate = false
          }
        })
      }
    },
    handleCheckedRolePermissionsChange(groupId) {
      const permissions = this.groupIdWithPermissionIds[groupId]
      const checked = permissions.filter(item => this.rolePermissions.includes(item))
      this.radio[groupId].checked = permissions.length === checked.length
      this.radio[groupId].isIndeterminate = checked.length > 0 && checked.length < permissions.length
    },
    cancel() {
      this.$emit('input', false)
    },
    assign() {
      this.buttonLoading = true
      assignPermission(this.id, { permission_ids: this.rolePermissions })
        .then(() => {
          this.$message.success('分配权限成功')
          this.buttonLoading = false
          this.cancel()
        }).catch(() => {
          this.buttonLoading = false
        })
    }
  }
}
</script>
<style scoped lang="scss">
.container {
  .el-card {
    margin-bottom: 20px;
    &:last-child {
      margin-bottom: 0;
    }
  }
}
</style>
