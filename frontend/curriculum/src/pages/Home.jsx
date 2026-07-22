import { Link } from 'react-router-dom';
import minhaFoto from '/src/assets/react.svg';
import Navbar from '/src/components/Navbar';


export default function Home() {
    return (
        <div>
            <Navbar /> {/* 2. Carimba o bloco no topo! */}
            
            <h1>Tela Inicial do Currículo</h1>
            <img src={minhaFoto} alt="react-logo" style={{ width: '100px' }} />
        </div>
    );
}