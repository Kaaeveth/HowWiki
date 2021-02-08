import Vue from 'vue';
import { Article } from '../../Article';
import { Component } from 'vue-property-decorator';

@Component
export default class CreateArticleComponent extends Vue {
    article: Article = {};
    tagInput: string = '';
    tagContainer: string[] = [];

    submit() {
        console.log(this.article);
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
