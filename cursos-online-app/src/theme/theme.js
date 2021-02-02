import { createMuiTheme } from '@material-ui/core/styles';

//caracteristicas del tema
const theme = createMuiTheme({
    palette : {
        primary : {
            light : "#63afff",
            main : "#1976d2",
            dark : "#004ba0",
            getContrastText : "#ecfad8"
        }
        


    }
});

export default theme;