import { Supplier } from './supplier.model';
import { Rating } from './rating.model';

export class Product {
	constructor(
		public productId?: number,
		public name?: string,
		public brand?: string,
		public description?: string,
		public price?: number,
		public productFeatures?: string,
		public productImgUrl?: string,
		public supplier?: Supplier,
		public ratings?: Rating[]
	) {}
}
