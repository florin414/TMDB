import React, { useState, useRef, useEffect } from 'react';
import { Button, Box, TextField } from '@mui/material';
import CommentComponent from './CommentComponent';
import RequireAuth from '../../auth/components/RequireAuth';
import styles from './ListCommentsComponent.module.scss'; 
import type { Comment } from '../types/movieTypes';

interface ListCommentsComponentProps {
  comments: Comment[];
  onAddComment: (comment: string) => Promise<void>;
  onViewMore: () => void;
  hasMore: boolean;
}

const ListCommentsComponent: React.FC<ListCommentsComponentProps> = ({
  comments,
  onAddComment,
  onViewMore,
  hasMore
}) => {
  const [newComment, setNewComment] = useState('');
  const [editing, setEditing] = useState(false);

  const inputRef = useRef<HTMLInputElement | null>(null);

  const handleAddComment = async () => {
    if (newComment.trim()) {
      await onAddComment(newComment);
      setNewComment('');
      setEditing(false);
    }
  };

  const handleKeyDown = async (event: React.KeyboardEvent) => {
    if (event.key === 'Enter') {
      await handleAddComment();
    } else if (event.key === 'Escape') {
      setEditing(false);
      setNewComment('');
    }
  };

  useEffect(() => {
    if (editing && inputRef.current) {
      inputRef.current.scrollIntoView({ behavior: 'smooth' });
    }
  }, [editing]);

  return (
    <Box className={styles.container}>
      {editing && (
        <Box className={styles.commentInputContainer}>
          <TextField
            ref={inputRef}
            autoFocus
            margin="dense"
            label="Your Comment"
            fullWidth
            multiline
            rows={4}
            value={newComment}
            onChange={(e) => setNewComment(e.target.value)}
            onKeyDown={handleKeyDown}
            className={styles.textField}
          />
          <Box className={styles.buttonsContainer}>
            <Button
              variant="outlined"
              color="primary"
              onClick={handleAddComment}
              className={styles.buttonOutlined}
            >
              Post
            </Button>
            <Button
              variant="outlined"
              color="secondary"
              onClick={() => {
                setEditing(false);
                setNewComment('');
              }}
              className={styles.buttonOutlined}
            >
              Cancel
            </Button>
          </Box>
        </Box>
      )}
      <Box className={styles.commentBox}>
        {comments.map((c, index) => (
          <CommentComponent
            key={index}
            name={'Anonymous'}
            avatarUrl={''}
            content={c.comment.value}
          />
        ))}
      </Box>
      <Box className={styles.commentActions}>
        {hasMore && (
          <Button variant="outlined" onClick={onViewMore}>
            View More Comments
          </Button>
        )}
        <RequireAuth fallback={<div>Please log in to post comments</div>}>
          <Button
            variant="outlined"
            onClick={() => setEditing(true)}
            disabled={editing}
          >
            Add Comment
          </Button>
        </RequireAuth>
      </Box>
    </Box>
  );
};

export default ListCommentsComponent;
