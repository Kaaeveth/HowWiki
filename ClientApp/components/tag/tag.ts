import Vue from 'vue';
import Component from 'vue-class-component';

const TagProps = Vue.extend({
    props: {
        tag: String,
        small: {
            type: Boolean,
            default: true
        }
    }
})

@Component
export default class TagComponent extends TagProps {

    click() {
        this.$emit('tagclick', this.$props.tag);
    }
}
