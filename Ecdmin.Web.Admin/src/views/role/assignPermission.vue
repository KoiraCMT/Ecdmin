<template>
  <div :loading="loading" class="container">
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
            <el-checkbox :label="permission.id" @change="handleChecked(group.id, permission.id)">
              {{ permission.display_name }}
            </el-checkbox>
          </el-col>
        </el-checkbox-group>
      </el-row>
    </el-card>
  </div>
</template>

<script>
import { listWithPermissions } from '@/api/permissionGroup'
import { has } from 'lodash'

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
      loading: false,
      groupPermissions: {},
      rolePermissions: [],
      radio: {},
      checkedGroupPermissions: {}
    }
  },
  created() {
    this.getAll()
  },
  methods: {
    getAll() {
      this.loading = true
      listWithPermissions().then(resp => {
        resp.data.forEach(group => {
          this.groupPermissions[group.id] = group
          this.checkedGroupPermissions[group.id] = []
          if (!has(this.radio, group.id)) {
            this.$set(this.radio, group.id, {
              checked: false,
              isIndeterminate: false
            })
          }
        })
        this.loading = false
      }).catch(() => {
        this.loading = false
      })
    },
    handleCheckAllChange(groupId) {
      const checkedCount = this.checkedGroupPermissions[groupId].length
      const permissionCount = this.groupPermissions[groupId].permissions.length
      this.groupPermissions[groupId].permissions.forEach(permission => {
        const index = this.rolePermissions.indexOf(permission.id)
        if (checkedCount < permissionCount && index === -1) {
          this.rolePermissions.push(permission.id)
          this.checkedGroupPermissions[groupId].push(permission.id)
          this.radio[groupId].checked = true
          this.radio[groupId].isIndeterminate = false
        } else if (checkedCount === permissionCount) {
          this.rolePermissions.splice(index, 1)
          const checkedIndex = this.checkedGroupPermissions[groupId].indexOf(permission.id)
          this.checkedGroupPermissions[groupId].splice(checkedIndex, 1)
        }
      })
    },
    handleChecked(groupId, permissionId) {
      const index = this.checkedGroupPermissions[groupId].indexOf(permissionId)
      if (index >= 0) {
        this.checkedGroupPermissions[groupId].splice(index, 1)
      } else {
        this.checkedGroupPermissions[groupId].push(permissionId)
      }
    },
    handleCheckedRolePermissionsChange(groupId) {
      const checkedCount = this.checkedGroupPermissions[groupId].length
      const permissionCount = this.groupPermissions[groupId].permissions.length
      this.radio[groupId].checked = permissionCount === checkedCount
      this.radio[groupId].isIndeterminate = checkedCount > 0 && checkedCount < permissionCount
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
