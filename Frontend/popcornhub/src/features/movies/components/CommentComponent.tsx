import React from 'react';
import UserDetailsComponent from './UserDetailsComponent';
import CommentContentComponent from './CommentContentComponent';
import styles  from './Comment.module.scss';

interface CommentComponentProps {
    name: string;
    avatarUrl: string;
    content: string;
}

const CommentComponent: React.FC<CommentComponentProps> = ({ name, avatarUrl, content }) => (
    <div className={styles.comment}>
        <UserDetailsComponent name={name} avatarUrl={avatarUrl} />
        <CommentContentComponent content={content} />
    </div>
);

export default CommentComponent;
