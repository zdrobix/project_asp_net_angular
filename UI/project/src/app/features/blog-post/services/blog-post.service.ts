import { Injectable } from '@angular/core';
import { AddBlogPost } from '../models/add-blogpost.model';
import { Observable } from 'rxjs/internal/Observable';
import { BlogPost } from '../models/blogpost.model';
import { HttpClient } from '@angular/common/http';
import { UpdateBlogPost } from '../models/update-blogpost.model';

@Injectable({
  providedIn: 'root'
})
export class BlogPostService {

  constructor(private http: HttpClient) { }

  createBlogPost(data: AddBlogPost) : Observable<BlogPost>{
    return this.http.post<BlogPost>(`https://localhost:7179/api/blogposts`, data);
  }

  getAllBlogPosts(): Observable<BlogPost[]> {
    return this.http.get<BlogPost[]>(`https://localhost:7179/api/blogposts`);
  }

  getBlogPostById(id: string): Observable<BlogPost> {
    return this.http.get<BlogPost>(`https://localhost:7179/api/blogposts/${id}`);
  }

  getBlogPostByUrlHandle(urlHandle: string): Observable<BlogPost> {
    return this.http.get<BlogPost>(`https://localhost:7179/api/blogposts/${urlHandle}`);
  }

  updateBlogPost(id: string, updatedBlogPost: UpdateBlogPost): Observable<BlogPost> {
    return this.http.put<BlogPost>(`https://localhost:7179/api/blogposts/${id}`, updatedBlogPost);
  }

  deleteBlogPost(id: string): Observable<BlogPost> {
    return this.http.delete<BlogPost>(`https://localhost:7179/api/blogposts/${id}`);
  }
}
