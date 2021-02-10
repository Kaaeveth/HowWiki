import Vue from 'vue';
import Component from 'vue-class-component';
import Comment from '../../Comment';

const ComponentOptions = Vue.extend({
    props: {
        textid: {
            type: Number
        },
        comments: {
            type: Array
        }
    }
});

@Component
export default class ListCommentsComponent extends ComponentOptions {

}