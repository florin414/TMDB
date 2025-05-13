import React from 'react';
import styles  from './Comment.module.scss';

interface CommentContentComponentProps {
  content: string;
}

const CommentContentComponent: React.FC<CommentContentComponentProps> = ({ content }) => (
  <div className={styles.commentContent}>
    <p>{content}</p>
  </div>
);

export default CommentContentComponent;
