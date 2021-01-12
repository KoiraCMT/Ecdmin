import request from '@/utils/request'

export const list = params => request.get('/permission', { params })

export const add = data => request.post('/permission', data)

export const update = (id, data) => request.put(`/permission/${id}`, data)

export const destroy = (id) => request.delete(`/permission/${id}`)
