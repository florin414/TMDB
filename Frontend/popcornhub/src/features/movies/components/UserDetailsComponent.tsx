import React from 'react';
import defaultAvatar from '../../../assets/default-avatar.jpg';
import styles from './UserDetailsComponent.module.scss'; 

interface UserDetailsComponentProps {
  name: string;
  avatarUrl: string;
}

const UserDetailsComponent: React.FC<UserDetailsComponentProps> = ({ name, avatarUrl }) => (
  <div className={styles.userDetails}>
    <img 
      src={avatarUrl || defaultAvatar} 
      alt={name} 
      className={styles.avatar} 
    />
    <strong>{name}</strong>
  </div>
);

export default UserDetailsComponent;
