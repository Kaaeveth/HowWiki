import Vue from 'vue';
import Component from 'vue-class-component';
import Article from '../../Article';
import Comment from '../../Comment';

const ComponentOptions = Vue.extend({
    components: {
        tag: require('../tag/tag.vue.html'),
        articleHolder: require('../articleHolder/articleHolder.vue.html'),
        starRating: require('../starRating/starRating.vue.html'),
        createComment: require('../createComment/createComment.vue.html'),
        listComments: require('../listComments/listComments.vue.html')
    }
});

@Component
export default class ShowArticleComponent extends ComponentOptions {
    isHelpful: boolean = false;
    rating: number = 0;
    article: Article = {};
    comments: Comment[] = [];

    mounted() {

        let textId: string | null = this.$route.params.textId;
        if (textId === null) {
            //todo: error routing
            return;
        }

        //Artikel fetchen
        fetch('/api/article/show/' + textId)
            .then(res => res.json() as Promise<Article>)
            .then(data => this.article = data);

        //Kommentare fetchen
        fetch('/api/comment/' + this.$route.params.textId)
            .then(res => res.json() as Promise<Comment[]>)
            .then(data => this.comments = data);
    }

    submitRating() {

    }

    setRating(val: number) {
        this.rating = val;
        console.log(val);
    }

    addComment(content: string) {
        this.comments.unshift({
            author: 'Clorox',
            comment: content,
            created: new Date()
        } as Comment);
    }
}
