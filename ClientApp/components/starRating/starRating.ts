import Vue from 'vue';
import Component from 'vue-class-component';

const ComponentOptions = Vue.extend({
})

@Component
export default class StarRatingComponent extends ComponentOptions {
    _rating: number = 0;

    set rating(val: number) {
        this.$emit('ratingchanged', val);
        this._rating = val;
    }

    get rating(): number {
        return this._rating;
    }
}