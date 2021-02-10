import Vue from 'vue';
import Component from 'vue-class-component';

const ComponentOptions = Vue.extend({
    props: {
        textid: Number
    }
});

@Component
export default class CreateCommentComponent extends ComponentOptions {
    comment: string = '';

    submitComment() {
        //Kommentar zum Parent geben und zum Server
        fetch('/api/comment/' + this.$props.textid + '/' + this.comment, {
            method: 'POST'
        })
            .then(res => {
                if (res.ok) {
                    this.$emit('commentsubmit', this.comment);
                    this.comment = '';
                } else {
                    alert('Fehler beim Kommentieren: ' + res.statusText);
                }
            });
    }
}