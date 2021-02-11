import Vue from 'vue';
import Article from '../../Models/Article';
import { Component } from 'vue-property-decorator';

@Component
export default class CreateArticleComponent extends Vue {
    article: Article = new Article();
    tagInput: string = '';
    tagContainer: string[] = [];

    submit() {

        console.log(this.article);

        let art: FormData = new FormData();
        art.append('title', this.article.title);
        art.append('content', this.article.content);
        art.append('references', this.article.references);
        art.append('tags', this.tagContainer.join(','));

        fetch('/api/article/create', {
            method: 'post',
            body: art
        })
            .then(res => {
                if (res.ok) {
                    alert('Erfolg');
                } else {
                    alert('Da ist was schiefgelaufen');
                }
            });
    }

    addTag() {
        if (this.tagInput.length > 0 && !(this.tagInput in this.tagContainer)) {
            this.tagContainer.push(this.tagInput);
        }
        this.tagInput = '';
    }

    removeTag(tag: string) {
        this.tagContainer.splice(this.tagContainer.indexOf(tag), 1);
    }
}
