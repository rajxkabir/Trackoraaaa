import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { Button, Input } from "../ui";
import { Navbar } from "../Navbar";

export default function AddTeamPage() {
    const navigate = useNavigate();
    const [employees, setEmployees] = useState([]);
    const [projects, setProjects] = useState([]);
    const [loading, setLoading] = useState(true);

    const [form, setForm] = useState({
        team_name: "",
        description: "",
        max_members: "",
        status: "ACTIVE",
        team_lead_id: "",
        project_id: "" // Added to link team to a project
    });

    // Fetch both Employees and Projects for the dropdowns
    useEffect(() => {
        const fetchInitialData = async () => {
            try {
                const [empRes, projRes] = await Promise.all([
                    fetch("https://localhost:7181/api/Employee/all"),
                    fetch("https://localhost:7181/api/Project/all")
                ]);

                if (empRes.ok) setEmployees(await empRes.json());
                if (projRes.ok) setProjects(await projRes.json());
                
            } catch (error) {
                console.error("Data fetch failed:", error);
            } finally {
                setLoading(false);
            }
        };
        fetchInitialData();
    }, []);

    const handleChange = (e) => {
        setForm({ ...form, [e.target.name]: e.target.value });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        
        // 1. Validation
        if (!form.team_lead_id) {
            alert("Please assign a Squad Lead.");
            return;
        }

        // 2. Construct Payload
        const payload = {
            team_name: form.team_name,
            description: form.description,
            max_members: parseInt(form.max_members),
            status: form.status,
            team_lead_id: parseInt(form.team_lead_id),
            created_by: 5001, 
            updated_by: 5001 
        };

        try {
            const response = await fetch("https://localhost:7181/api/Team/add", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(payload),
            });

            if (response.ok) {
                alert("Squad successfully mobilized!");
                navigate("/team");
            } else {
                const errorText = await response.text();
                console.error("Server Error:", errorText);
                alert("Transmission failed. Database rejected the entry.");
            }
        } catch (error) {
            console.error("Connection Error:", error);
        }
    };

    return (
        <div className="min-h-screen bg-background text-foreground transition-all">
            <Navbar user="Admin" />
            <div className="max-w-4xl mx-auto pt-12 px-6 pb-20">
                <div className="mb-10">
                    <h1 className="text-4xl font-black tracking-tighter uppercase text-primary">Form New Squad</h1>
                    <p className="text-muted-foreground mt-2 font-medium">Define the unit name, capacity, and commander.</p>
                </div>

                <form onSubmit={handleSubmit} className="space-y-8 bg-card/40 p-8 rounded-[2.5rem] border border-border shadow-2xl backdrop-blur-md">
                    
                    {/* Row 1: Team Name & Max Members */}
                    <div className="grid gap-6 md:grid-cols-2">
                        <div className="space-y-2">
                            <label className="text-xs font-black uppercase tracking-widest ml-1 text-primary">Squad Name</label>
                            <Input 
                                name="team_name" 
                                value={form.team_name} 
                                onChange={handleChange} 
                                placeholder="e.g. Alpha-9" 
                                required 
                                className="h-12 rounded-2xl bg-background text-foreground" 
                            />
                        </div>
                        <div className="space-y-2">
                            <label className="text-xs font-black uppercase tracking-widest ml-1 text-primary">Max Capacity</label>
                            <Input 
                                name="max_members" 
                                type="number" 
                                value={form.max_members} 
                                onChange={handleChange} 
                                placeholder="e.g. 12" 
                                required 
                                className="h-12 rounded-2xl bg-background text-foreground" 
                            />
                        </div>
                    </div>

                    {/* Row 2: Team Lead & Status */}
                    <div className="grid gap-6 md:grid-cols-2">
                        <div className="space-y-2">
                            <label className="text-xs font-black uppercase tracking-widest ml-1 text-primary">Assign Commander (Lead)</label>
                            <select 
                                name="team_lead_id" 
                                value={form.team_lead_id} 
                                onChange={handleChange}
                                className="w-full h-12 rounded-2xl bg-background border border-input px-4 text-foreground outline-none focus:ring-2 focus:ring-primary/20 transition-all"
                                required
                            >
                                <option value="">Select an Employee...</option>
                                {employees
                                    .filter(emp => emp.EMP_ROLE?.toUpperCase() !== "ADMIN") // Excludes Admins
                                    .map(emp => (
                                        <option key={emp.EMP_ID || emp.id} value={emp.EMP_ID || emp.id}>
                                            {emp.EMP_FIRSTNAME} {emp.EMP_LASTNAME} (ID: {emp.EMP_ID || emp.id})
                                        </option>
                                    ))}
                            </select>
                        </div>

                        <div className="space-y-2">
                            <label className="text-xs font-black uppercase tracking-widest ml-1 text-primary">Operational Status</label>
                            <select 
                                name="status" 
                                value={form.status} 
                                onChange={handleChange}
                                className="w-full h-12 rounded-2xl bg-background border border-input px-4 text-foreground outline-none focus:ring-2 focus:ring-primary/20 transition-all"
                            >
                                <option value="ACTIVE">ACTIVE</option>
                                <option value="INACTIVE">INACTIVE</option>
                            </select>
                        </div>
                    </div>

                    {/* Operational Briefing (Description) */}
                    <div className="space-y-2">
                        <label className="text-xs font-black uppercase tracking-widest ml-1 text-primary">Operational Briefing</label>
                        <textarea 
                            name="description" 
                            value={form.description} 
                            onChange={handleChange} 
                            placeholder="Detail the squad's primary objective..."
                            className="w-full min-h-[120px] p-4 rounded-2xl bg-background border border-input text-foreground outline-none focus:ring-2 focus:ring-primary/20 transition-all resize-none"
                        />
                    </div>

                    <div className="flex justify-between items-center pt-6 border-t border-border">
                        <Button type="button" variant="ghost" onClick={() => navigate("/team")}>Abort Mission</Button>
                        <Button type="submit" className="h-12 px-10 rounded-2xl font-bold shadow-lg shadow-primary/20 bg-primary hover:opacity-90 transition-all">
                            Deploy Squad
                        </Button>
                    </div>
                </form>
            </div>
        </div>
    );
}