export interface BlogPost {
    id: string
    title: string;
    shortDescription: string;
    content: string;
    featuredUrlImage: string;
    urlHandle: string;
    author: string;
    publishedDate: Date;
    isVisible: boolean;
}