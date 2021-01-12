import request from '@/utils/request'

export const list = params => request.get('/permission-group', { params })

export const add = (data) => request.post('/permission-group', data)

export const update = (id, data) => request.put(`/permission-group/${id}`, data)

export const destroy = (id) => request.delete(`/permission-group/${id}`)

export const listWithPermissions = () => request.get('/permission-group/with-permissions')
