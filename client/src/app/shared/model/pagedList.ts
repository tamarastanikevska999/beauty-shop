import { SortOrderEnum } from "./sortOrderEnum";

export interface PagedList<T>{
    totalCount?: number;
    pageSize?: number;
    page?: number;
    sortOrder?: SortOrderEnum;
    sortBy?: string;
    items?: T;
  }
