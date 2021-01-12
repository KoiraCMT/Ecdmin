<template>
    <div>
        <el-upload
                :file-list="fileList"
                :headers="headers"
                :action="uri"
                list-type="picture-card"
                name="image"
                :on-success="handleUploadSuccess"
                :on-remove="handleRemove">
            <i class="el-icon-plus"></i>
        </el-upload>
        <el-dialog :visible.sync="dialogVisible">
            <img width="100%" v-for="pic in imageUrls" :key="pic" :src="pic">
        </el-dialog>
    </div>
</template>
<script>
import {imageUri, removeFile} from '../../api/upload'
import { remove } from 'lodash'
import {mapGetters} from 'vuex'
export default {
    name: "UploadImages",
    props: {
        value: {
            required: true,
            type: Array,
        }
    },
    watch: {
        imageUrls(val) {
            this.$emit('input', val);
        },
        value(val) {
            this.imageUrls = val
            this.fileList = []
            this.value.map(item => {
                this.fileList.push({url: item})
            })
        }
    },
    data() {
        return {
            imageUrls: [],
            dialogVisible: false,
            uri: '',
            headers: {},
            fileList: []
        }
    },
    computed: {
      ...mapGetters([
          'accessToken'
      ])
    },
    methods: {
        handleUploadSuccess(res, file) {
            this.imageUrls.push(res.data)
        },
        handleRemove(file) {
            remove(this.imageUrls, function (item) {
                return item === file.url
            })
            removeFile({file: file.url})
        }
    },
    mounted() {
        this.uri = imageUri()
        this.imageUrls = this.value
        this.value.map(item => {
            this.fileList.push({url: item})
        })
        this.headers.Authorization = `Bearer ${this.accessToken}`
    }
}
</script>