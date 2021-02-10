import './css/style.css';
import Vue from 'vue';
import VueRouter from 'vue-router';
Vue.use(VueRouter);

const routes = [
    { path: '/', component: require('./components/home/home.vue.html') },
    { path: '/article/list', component: require('./components/listArticle/listArticles.vue.html') },
    { path: '/article/show/:textId', component: require('./components/showArticle/showArticle.vue.html') },
    { path: '/article/create', component: require('./components/createArticle/createArticle.vue.html') }
];

new Vue({
    el: '#app-root',
    router: new VueRouter({ mode: 'history', routes: routes }),
    render: h => h(require('./components/app/app.vue.html'))
});
