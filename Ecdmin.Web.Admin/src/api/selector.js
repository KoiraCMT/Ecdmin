import request from '@/utils/request'

export const roles = () => request.get('/selector/roles')
