import { Component, OnInit } from '@angular/core';
import { BlogPostService } from '../services/blog-post.service';
import { Observable } from 'rxjs/internal/Observable';
import { BlogPost } from '../models/blogpost.model';

@Component({
  selector: 'app-blogpost-list',
  templateUrl: './blogpost-list.component.html',
  styleUrls: ['./blogpost-list.component.css']
})
export class BlogpostListComponent implements OnInit {

  blogPosts$?: Observable<BlogPost[]>
  
  constructor(private blogPostService: BlogPostService) { 
  }

  ngOnInit(): void {
     this.blogPosts$ = this.blogPostService.getAllBlogPosts();
  }
}
