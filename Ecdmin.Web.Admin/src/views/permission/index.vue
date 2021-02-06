<template>
  <div class="app-container">
    <el-row :gutter="5">
      <el-col :span="8">
        <el-card>
          <permission-group @get-id="getGroupId" @get-name="getGroupName" />
        </el-card>
      </el-col>
      <el-col :span="16">
        <el-card>
          <permission :permission-group-id="permissionGroupId" :permission-group-name="permissionGroupName" />
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
      permissionGroupId: 0,
      permissionGroupName: ''
    }
  },
  methods: {
    getGroupId(id) {
      this.permissionGroupId = id
    },
    getGroupName(name) {
      this.permissionGroupName = name
    }
  }
}
</script>

<style scoped>

</style>
