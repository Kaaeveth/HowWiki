﻿import Vue from 'vue';
import { Article } from '../../Article';
import { Component } from 'vue-property-decorator';

@Component
export default class ListArticlesComponent extends Vue {
    articles: Article[] = [];

    mounted() {
        fetch('/api/article/list')
            .then(response => response.json() as Promise<Article[]>)
            .then(data => this.articles = data);
    }
}
