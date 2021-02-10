import Vue from 'vue';
import Component from 'vue-class-component';

@Component
export default class StarRatingComponent extends Vue {
    _rating: number = 0;

    get rating(): number {
        return this._rating;
    }

    set rating(val: number) {
        this._rating = val;
        this.$emit('ratingchanged', this._rating);
    }
}