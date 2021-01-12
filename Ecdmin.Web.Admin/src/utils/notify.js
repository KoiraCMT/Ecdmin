class Notify {
  success(message, th) {
    th.$notify({
      message: message,
      type: 'success',
      duration: 2000
    })
  }

  createSuccess(th) {
    this.success('新建成功', th)
  }

  editSuccess(th) {
    this.success('修改成功', th)
  }

  deleteSuccess(th) {
    this.success('删除成功', th)
  }
}

export default new Notify()
