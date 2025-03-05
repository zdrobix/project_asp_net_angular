import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AddCategoryRequest } from '../models/add-category-request.model';
import { HttpClient } from '@angular/common/http';
import { Category } from '../models/category.model';
import { environment } from 'src/environments/environment';
import { UpdateCategoryRequest } from '../models/update-category-request.model';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  constructor(private http: HttpClient) { }

  addCategory (model: AddCategoryRequest): Observable<void> {
    //return this.http.post<void>(`${environment.apiBaseUrl}/api/categories`, model);
    return this.http.post<void>(`https://localhost:7179/api/categories`, model);
  }

  getAllCategories(): Observable<Category[]> {
    //return this.http.get<Category[]>(`${environment.apiBaseUrl}/api/categories`);
    return this.http.get<Category[]>(`https://localhost:7179/api/categories`);
  }

  getCategoryById(id: string): Observable<Category> {
    //return this.http.get<Category>(`${environment.apiBaseUrl}/api/categories/${id}`);
    return this.http.get<Category>(`https://localhost:7179/api/categories/${id}`);
  }

  updateCategory(id: string, updateCategoryRequest: UpdateCategoryRequest) : Observable<Category> {
    //return this.http.put<Category>(`${environment.apiBaseUrl}/api/categories/${id}`, updateCategoryRequest);
    return this.http.put<Category>(`https://localhost:7179/api/categories/${id}`, updateCategoryRequest);
  }

  deleteCategory(id: string): Observable<Category> {
    //return this.http.delete<Category>(`${environment.apiBaseUrl}/api/categories/${id}`);
    return this.http.delete<Category>(`https://localhost:7179/api/categories/${id}`);
  }
}
