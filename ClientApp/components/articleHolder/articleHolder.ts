import Vue from 'vue';
import Component from 'vue-class-component';
import Article from '../../Models/Article';

const ArticleProps = Vue.extend({
    props: {
        article: {
            type: Article
        }
    }
})

@Component({
    components: {
        TagComponent: require('../tag/tag.vue.html')
    }
})
export default class ArticleHolder extends ArticleProps {

}