import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs/internal/Subscription';
import { CategoryService } from '../services/category.service';
import { Category } from '../models/category.model';
import { UpdateCategoryRequest } from '../models/update-category-request.model';

@Component({
  selector: 'app-edit-category',
  templateUrl: './edit-category.component.html',
  styleUrls: ['./edit-category.component.css']
})
export class EditCategoryComponent implements OnInit, OnDestroy{

  id: string | null = null;
  paramsSubscription?: Subscription;
  editCategorySubscription?: Subscription;
  deleteCategorySubscription?: Subscription;
  category?: Category;

  constructor(private route: ActivatedRoute, private categoryService: CategoryService, private router: Router) { 
  }

  ngOnInit(): void {
    this.route.params.subscribe({
      next: (params) => {
        this.id = params['id'];
        if (this.id) {
          //get the data from the api
          this.categoryService.getCategoryById(this.id).subscribe(
            {
              next: (response) => {
                this.category = response;
              }
            }
          )
        }
      }
    });
  }
  
  ngOnDestroy(): void {
    this.paramsSubscription?.unsubscribe();
    this.editCategorySubscription?.unsubscribe();
    this.deleteCategorySubscription?.unsubscribe();
  }

  onFormSubmit() : void {
    console.log(this.category);
    const updateCategoryRequest: UpdateCategoryRequest = {
      name: this.category?.name ?? '',
      urlHandle: this.category?.urlHandle ?? ''
    };

    if (this.id)
      this.editCategorySubscription = this.categoryService.updateCategory(this.id, updateCategoryRequest).subscribe({
        next: (response) => {
          this.router.navigateByUrl('/admin/categories');
        }
      });
  }

  onDelete() : void {
    if (this.id)
      this.deleteCategorySubscription = this.categoryService.deleteCategory(this.id).subscribe({
        next: (response) => {
          this.router.navigateByUrl('/admin/categories');
        }
      });
  }
}
