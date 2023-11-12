import { SortOrderEnum } from "./sortOrderEnum";

export interface PagedList<T>{
    totalCount?: number;
    pageSize?: number;
    page?: number;
    totalPages?: number;
    sortOrder?: SortOrderEnum;
    sortBy?: string;
    items?: T;
  }
