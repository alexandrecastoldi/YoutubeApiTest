import { Video } from './Video';

export interface SearchResult {
    nextPage: string;
    videos: Video[];
}