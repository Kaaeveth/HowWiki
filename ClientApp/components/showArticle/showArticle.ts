import Vue from 'vue';
import Component from 'vue-class-component';
import Article from '../../Models/Article';
import Comment from '../../Models/Comment';

const ComponentOptions = Vue.extend({
    components: {
        tag: require('../tag/tag.vue.html'),
        //articleHolder: require('../articleHolder/articleHolder.vue.html'),
        starRating: require('../starRating/starRating.vue.html'),
        createComment: require('../createComment/createComment.vue.html'),
        listComments: require('../listComments/listComments.vue.html')
    }
});

@Component
export default class ShowArticleComponent extends ComponentOptions {
    isHelpful: boolean = false;
    rating: number = 0;
    article: Article = new Article();
    comments: Comment[] = [];
    textId: string = '';

    mounted() {

        let textId: string | null = this.$route.params.textId;
        if (textId === null) {
            //todo: error routing
            location.pathname = '/error';
            return;
        }
        this.textId = textId;

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
        console.log('Sterne: ' + this.rating + ' Hp: ' + this.isHelpful);

        fetch('/api/rating/rate/' + this.textId + '/' + this.rating + '/' + this.isHelpful, {
            method: 'put'
        })
            .then(res => {
                if (res.ok)
                    alert('ok');
                else
                    alert('nicht ok: ' + res.status);
            });
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
