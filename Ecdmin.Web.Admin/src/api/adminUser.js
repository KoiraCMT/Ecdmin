import request from '@/utils/request'

export const list = (params) => request.get('/admin-user', { params })

export const add = (data) => request.post('/admin-user', data)

export const update = (id, data) => request.put(`/admin-user/${id}`, data)

export const destroy = (id) => request.delete(`/admin-user/${id}`)
