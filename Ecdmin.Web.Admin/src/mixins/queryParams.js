export const queryParams = {
  data() {
    return {
      searchFormSize: 'mini',
      tableSize: 'media',
      buttonSize: 'mini',
      formSize: 'mini',
      queryParamsChange: false,
      queryParams: {

      },
      tableData: [],
      pagination: {
        currentPage: 1,
        pageSize: 15,
        total: 0
      },
      nowRowData: {
        index: 0,
        row: {}
      },
      dialogAddFormVisible: false,
      dialogEditFormVisible: false,
      formLabelWidth: '120px',
      loading: false,
      buttonLoading: false
    }
  },
  methods: {

  },
  watch: {
    queryParams: {
      handler: function() {
        this.queryParamsChange = true
      },
      deep: true
    }
  },
  computed: {
    queryPage: function() {
      return this.queryParamsChange ? 1 : this.pagination.currentPage
    }
  }
}
