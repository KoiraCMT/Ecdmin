import notify from './notify'
export function responseDataFormat(response, th) {
  th.tableData = response.data

  const meta = response.meta
  th.pagination = {
    currentPage: meta.page,
    pageSize: meta.page_size,
    total: meta.total
  }

  th.loading = false
  th.queryParamsChange = false
}

export const editSuccess = th => {
  th.getList()
  notify.editSuccess(th)
  th.buttonLoading = false
  th.dialogEditFormVisible = false
  th.$refs['editForm'].resetFields()
}

export const simpleUpdate = (th, api) => {
  th.$refs['editForm'].validate((valid) => {
    if (valid) {
      th.buttonLoading = true
      api(th.nowRowData.row.id, th.editForm).then(() => {
        editSuccess(th)
      }).catch(() => {
        th.buttonLoading = false
      })
    }
  })
}

export const simpleAdd = (th, api) => {
  th.$refs['addForm'].validate(valid => {
    if (valid) {
      th.buttonLoading = true
      api(th.addForm).then(() => {
        addSuccess(th)
      }).catch(() => {
        th.buttonLoading = false
      })
    }
  })
}

export const simpleDelete = (th, api) => {
  th.$confirm('是否确认删除此数据?', '提示', {
    confirmButtonText: '确定',
    cancelButtonText: '取消',
    type: 'warning'
  }).then(() => {
    th.loading = true
    api.then(() => {
      deleteSuccess(th)
    })
  })
}

export const simpleList = (th, api) => {
  th.loading = true
  api({ ...th.queryParams, page: th.queryPage, page_size: th.pagination.pageSize }).then(resp => {
    responseDataFormat(resp, th)
  }).catch(() => {
    th.loading = false
  })
}

export const addSuccess = th => {
  th.getList()
  th.dialogAddFormVisible = false
  th.buttonLoading = false
  notify.createSuccess(th)
  th.$refs['addForm'].resetFields()
}

export const deleteSuccess = th => {
  th.loading = false
  notify.deleteSuccess(th)
  th.getList()
}
