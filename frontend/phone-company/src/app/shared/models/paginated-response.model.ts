export interface IPaginatedResponse<T> {
  items: T[];
  pageIndex: number;
  pageSize: number;
  pageCount: number;
}
