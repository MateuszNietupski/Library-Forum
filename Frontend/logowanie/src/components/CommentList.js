import React from 'react';
import { List, ListItem, ListItemText, Typography, Paper, Avatar, Box } from '@mui/material';

const CommentList = ({ comments }) => {
    return (
        <List>
            {comments && comments.length > 0 ? (
                comments.map((comment) => (
                    <ListItem key={comment.id}>
                        <Paper elevation={3} style={{ padding: '10px', width: '100%' }}>
                            <Box display="flex" alignItems="center" justifyContent="space-between">
                                <Box display="flex" alignItems="center" flexDirection="column">
                                    <Avatar />
                                    <Box marginTop="10px">
                                        <Typography variant="subtitle1">{comment.user}</Typography>
                                    </Box>
                                </Box>
                                <Box flex="1">
                                    <Typography variant="body1" align="center">
                                        {comment.content}
                                    </Typography>
                                </Box>
                                <Box>
                                    <Typography variant="caption">{comment.data.toLocaleString()}</Typography>
                                </Box>
                            </Box>
                        </Paper>
                    </ListItem>
                ))
            ) : (
                <ListItem>
                    <ListItemText>
                        <Typography variant="body1">Brak komentarzy</Typography>
                    </ListItemText>
                </ListItem>
            )}
        </List>
    );
};

export default CommentList;
