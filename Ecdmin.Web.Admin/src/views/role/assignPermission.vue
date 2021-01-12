<template>
  <div v-loading="loading" class="container">
    <el-card v-for="group in groupPermissions" :key="group.id">
      <div slot="header">
        <span class="permission-group">{{ group.name }}</span>
      </div>
      <el-row>
        <el-checkbox-group v-model="rolePermissions">
          <el-col
            v-for="permission in group.permissions"
            :key="permission.id"
            class="permission-item"
            :span="4"
          >
            <el-checkbox :label="permission.id">
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
      groupPermissions: [],
      rolePermissions: []
    }
  },
  created() {
    this.getAll()
  },
  methods: {
    getAll() {
      this.loading = true
      listWithPermissions().then(resp => {
        this.groupPermissions = resp.data
        this.loading = false
      }).catch(() => {
        this.loading = false
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
