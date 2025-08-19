export type Pagination<t> = {
  pageIndex: number;
  pageSize: number;
  count: number;
  data: t[];
}