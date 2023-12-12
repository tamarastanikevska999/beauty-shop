import { ProductReview } from "./productReview";

export interface Product {
    id: string;
    name: string;
    description: string;
    price: number;
    pictureUrl: string;
    productType: string;
    productBrand: string;
    comments: ProductReview[];
    rating: number;
}
