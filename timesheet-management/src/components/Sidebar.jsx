import {
    Clock,
    LayoutDashboard,
    FolderKanban,
    Users,
    FileText,
    Settings,
    Plus,
    ChevronRight,
    BarChart,
} from "lucide-react";
import { cn } from "../lib/utils";
import { Button } from "./ui";
import { useNavigate, useLocation } from "react-router-dom";
 
const navigation = [
    { id: "dashboard", name: "Dashboard", icon: LayoutDashboard },
    { id: "employees", name: "Employees", icon: FileText },
    { id: "projects", name: "Projects", icon: FolderKanban },
    { id: "team", name: "Team", icon: Users },
    { id: "timesheet", name: "Timesheet", icon: Clock },
    { id: "reports", name: "Reports", icon: BarChart },
];
 

export function Sidebar({ isOpen }) {
    const navigate = useNavigate();
    const location = useLocation();
 
    const currentPath = location.pathname.replace("/", "") || "dashboard";
 
    return (
        <>
            {isOpen && (
                <div
                    className="fixed inset-0 z-30 bg-foreground/20 backdrop-blur-sm md:hidden"
                    onClick={() => navigate("/" + currentPath)}
                />
            )}
 
            <aside
                className={cn(
                    "fixed left-0 top-14 z-40 h-[calc(100vh-3.5rem)] w-64 border-r border-border/40 bg-background transition-transform duration-300 ease-in-out md:translate-x-0",
                    isOpen ? "translate-x-0" : "-translate-x-full"
                )}
            >
                <div className="flex h-full flex-col">
 
                
                    <nav className="flex-1 space-y-1 px-3">
                        <div className="mb-2">
                            <span className="px-3 text-xs font-medium uppercase tracking-wider text-muted-foreground">
                                Menu
                            </span>
                        </div>
 
                        {navigation.map((item) => {
                            const isActive = currentPath === item.id;
 
                            return (
                                <button
                                    key={item.id}
                                    onClick={() => navigate("/" + item.id)}
                                    className={cn(
                                        "flex w-full items-center gap-3 rounded-xl px-3 py-2.5 text-sm font-medium transition-all",
                                        isActive
                                            ? "bg-secondary text-foreground"
                                            : "text-muted-foreground hover:bg-secondary/50 hover:text-foreground"
                                    )}
                                >
                                    <item.icon
                                        className={cn(
                                            "w-5 h-5",
                                            isActive ? "text-foreground" : "text-muted-foreground"
                                        )}
                                    />
                                    {item.name}
                                    {isActive && (
                                        <ChevronRight className="ml-auto w-4 h-4" />
                                    )}
                                </button>
                            );
                        })}
                    </nav>
 
 
                    <div className="border-t border-border/40 p-4">
                        <button
                            onClick={() => navigate("/settings")}
                            className="flex w-full items-center gap-3 rounded-xl px-3 py-2.5 text-sm font-medium text-muted-foreground transition-colors hover:bg-secondary/50 hover:text-foreground"
                        >
                            <Settings className="w-5 h-5" />
                            Settings
                        </button>
                    </div>
                </div>
            </aside>
        </>
    );
}
 