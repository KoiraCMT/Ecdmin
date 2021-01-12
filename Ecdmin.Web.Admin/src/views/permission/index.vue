<template>
  <div class="app-container">
    <el-row :gutter="5">
      <el-col :span="8">
        <el-card>
          <permission-group @get-id="getGroupId" />
        </el-card>
      </el-col>
      <el-col :span="16">
        <el-card>
          <permission :permission-group-id="permissionGroupId" />
        </el-card>
      </el-col>
    </el-row>
  </div>
</template>

<script>
import PermissionGroup from '@/views/permission/PermissionGroup'
import Permission from '@/views/permission/Permission'
export default {
  name: 'Index',
  components: {
    Permission,
    PermissionGroup
  },
  data() {
    return {
      permissionGroupId: 0
    }
  },
  methods: {
    getGroupId(id) {
      this.permissionGroupId = id
    }
  }
}
</script>

<style scoped>

</style>
