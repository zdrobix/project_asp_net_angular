import { Component, OnDestroy, OnInit } from '@angular/core';
import { ParamMap, ActivatedRoute } from '@angular/router';
import { Observable, Subscription } from 'rxjs';
import { BlogPostService } from '../services/blog-post.service';
import { BlogPost } from '../models/blogpost.model';
import { CategoryService } from '../../category/services/category.service';
import { Category } from '../../category/models/category.model';

@Component({
  selector: 'app-edit-blogpost',
  templateUrl: './edit-blogpost.component.html',
  styleUrls: ['./edit-blogpost.component.css']
})
export class EditBlogpostComponent implements OnInit, OnDestroy {
  
  id: string | null = null;
  model?: BlogPost;
  routeSubscription?: Subscription;
  categories$?: Observable<Category[]>;
  selectedCategories?: string[] = [];

  constructor(private route: ActivatedRoute, private blogPostService: BlogPostService, private categoryService: CategoryService) {

   }
   
   ngOnInit(): void {
    this.categories$ = this.categoryService.getAllCategories();
    this.routeSubscription = this.route.paramMap.subscribe( {
      next: (params) => {
        this.id = params.get('id');

        // get blogpost from api
        if (this.id)
        {
          this.blogPostService.getBlogPostById(this.id).subscribe({
            next: (response) => {
              this.model = response;
              this.selectedCategories = response.categories.map(x => x.id);
              console.log(this.selectedCategories)
            }
          });
        }

        }
      });
    }
    
  onFormSubmit(): void {}

  ngOnDestroy(): void {
    this.routeSubscription?.unsubscribe();
  }
}