<template>
  <div>
    <el-upload
      :headers="headers"
      class="avatar-uploader"
      :action="uri"
      :show-file-list="false"
      :on-success="handleUploadSuccess"
      :before-upload="handelBeforeUpload"
      :name="name"
    >
      <img v-if="imageUrl" :src="imageUrl" class="avatar">
      <i v-else class="el-icon-plus avatar-uploader-icon" />
    </el-upload>
  </div>
</template>
<style>
.avatar-uploader .el-upload {
  border: 1px dashed #d9d9d9;
  border-radius: 6px;
  cursor: pointer;
  position: relative;
  overflow: hidden;
}
.avatar-uploader .el-upload:hover {
  border-color: #20a0ff;
}
.avatar-uploader-icon {
  font-size: 28px;
  color: #8c939d;
  width: 178px;
  height: 178px;
  line-height: 178px;
  text-align: center;
}
.avatar {
  width: 178px;
  height: 178px;
  display: block;
}
</style>
<script>
import { mapGetters } from 'vuex'
import { avatar } from '@/api/upload'

export default {
  name: 'UploadImage',
  props: {
    value: {
      required: true,
      type: String
    },
    size: {
      type: Number,
      default: 500
    },
    mime: {
      type: String,
      default: 'jpeg|png|jpg'
    },
    name: {
      type: String,
      default: 'image'
    }
  },

  data() {
    return {
      uri: '',
      imageUrl: '',
      headers: {}
    }
  },
  computed: {
    ...mapGetters([
      'token'
    ])
  },
  watch: {
    imageUrl(val) {
      this.$emit('input', val)
    },
    value(val) {
      this.imageUrl = val
    }
  },
  mounted() {
    this.uri = avatar
    this.imageUrl = this.value
    this.headers.Authorization = `Bearer ${this.token}`
  },
  methods: {
    handleUploadSuccess(res, file) {
      this.imageUrl = res.data
    },
    isImage(mime) {
      const mimes = this.mime.split('|')
      mime = mime.split('/')[1]
      return mimes.indexOf(mime) > -1
    },
    bigSize(size) {
      return size / 1024 < this.size
    },
    handelBeforeUpload(file) {
      const isJPG = this.isImage(file.type)
      const bigSize = this.bigSize(file.size)

      if (!isJPG) {
        this.$message.error('上传的文件不是图片')
      }
      if (!bigSize) {
        this.$message.error('上传图片大小太大')
      }

      return isJPG && bigSize
    }
  }
}
</script>
