import request from '@/utils/request'

export const list = (params) => request.get('/administrator', { params })

export const add = (data) => request.post('/administrator', data)

export const update = (id, data) => request.put(`/administrator/${id}`, data)

export const destroy = (id) => request.delete(`/administrator/${id}`)
