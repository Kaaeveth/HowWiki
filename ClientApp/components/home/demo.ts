import Vue from 'vue';
import Component from 'vue-class-component';

@Component
export default class DemoComponent extends Vue {
    message: string = 'Hallo';
}
