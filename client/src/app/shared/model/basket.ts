export interface ProductInBasket {
    basketId: number;
    productId: number;
    name: string;
    price: number;
    quantity: number;
    pictureUrl: string;
    brand: string;
    type: string;
}

export interface Basket {
    id: string;
    userEmail: string;
    items: ProductInBasket[];
}

export interface BasketTotals {
    shipping: number;
    subtotal: number;
    total: number;
}