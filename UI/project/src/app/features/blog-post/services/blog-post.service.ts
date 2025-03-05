import { Injectable } from '@angular/core';
import { AddBlogPost } from '../models/add-blogpost.model';
import { Observable } from 'rxjs/internal/Observable';
import { BlogPost } from '../models/blogpost.model';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class BlogPostService {

  constructor(private http: HttpClient) { }

  createBlogPost(data: AddBlogPost) : Observable<BlogPost>{
    return this.http.post<BlogPost>(`https://localhost:7179/api/blogposts`, data);
  }
}
