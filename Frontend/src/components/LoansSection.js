import {Accordion, AccordionDetails, AccordionSummary, Typography, Grid, Chip} from "@mui/material";
import {DateFormat} from "../utils/DateFormat";
import {ExpandMore as ExpandMoreIcon} from "@mui/icons-material";

const LoansSection = ({ title, loans, chipColor }) => {
    return (
        <>
            <Typography variant="h6" sx={{ mt: 2, mb: 1 }}>{title}</Typography>

            {loans.length === 0 && (
                <Typography variant="body2" sx={{ mb: 2 }}>
                    Brak pozycji
                </Typography>
            )}

            {loans.map(l => (
                <Accordion key={l.id} sx={{ mb: 1 }}>
                    <AccordionSummary expandIcon={<ExpandMoreIcon />}>
                        <Grid container spacing={2}>
                            <Grid item xs={4}>#{l.id.slice(0, 8)}</Grid>
                            <Grid item xs={4}>
                                {new Date(l.start).toLocaleDateString('pl-PL')}
                            </Grid>
                            <Grid item xs={4}>
                                <Chip
                                    label={l.ReturnDate ? 'Zakończone' : 'Aktywne'}
                                    color={chipColor}
                                    size="small"
                                />
                            </Grid>
                        </Grid>
                    </AccordionSummary>

                    <AccordionDetails>
                        {l.items.map(it => (
                            <Typography key={it.title}>
                                {it.title} — {it.author} × {it.quantity}
                            </Typography>
                        ))}

                        {l.ReturnDate && (
                            <Typography variant="body2" sx={{ mt: 1 }}>
                                Zwrot: 
                                {DateFormat(l.ReturnDate)}
                            </Typography>
                        )}
                    </AccordionDetails>
                </Accordion>
            ))}
        </>
    );
}
export default LoansSection;