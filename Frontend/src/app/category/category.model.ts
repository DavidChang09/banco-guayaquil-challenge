import { BaseModel } from "../../shared/base-model";

export interface CategoryModel extends BaseModel {
  name: string;
  categoryId?: number | null;
  parentCategoryName: string;
}
