import { createMuiTheme } from '@material-ui/core/styles';

//caracteristicas del tema
const theme = createMuiTheme({
    palette : {
        primary : {
            //
            light : "#63afff",
            //azul claro 
            main : "#1976d2",
            //azul oscuro foco
            dark : "#004ba0",
            getContrastText : "#ecfad8"
        }
        


    }
});

export default theme;