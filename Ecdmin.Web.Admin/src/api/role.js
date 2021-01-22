import request from '@/utils/request'

export const list = params => request.get('/role', { params })

export const add = (data) => request.post('/role', data)

export const update = (id, data) => request.put(`/role/${id}`, data)

export const destroy = (id) => request.delete(`/role/${id}`)

export const findWithPermissions = (id) => request.get(`/role/with-permissions/${id}`)

export const assignPermission = (id, data) => request.post(`/role/assign-permission/${id}`, data)
